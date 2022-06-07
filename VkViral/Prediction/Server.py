from flask import Flask, jsonify, request
from catboost import CatBoostRegressor
import re
from nltk.tokenize import word_tokenize
import nltk
import urllib.request
import numpy as np
from PIL import Image
import pandas as pd
from sklearn.feature_extraction.text import CountVectorizer

app = Flask(__name__)

lemmatizer = nltk.stem.WordNetLemmatizer()
cb = CatBoostRegressor()
cb.load_model('model')
word_features = list(filter(lambda x: bool(re.match(r'word_', x)), cb.feature_names_))
word_features_count = len(word_features)
vocabulary = list(map(lambda x: x[5:], word_features))

def first_photo_average_colour(photos):
    try:
        if any(photos):
            urllib.request.urlretrieve(photos[0], "temp.png")
            img = np.asarray(Image.open('temp.png'))
            average_colour_row = np.average(img, axis=0)
            average_colour = np.average(average_colour_row, axis=0)
            return list(average_colour)
        return [0, 0, 0]
    except:
        return [0, 0, 0]

def preprocess_text(text):
    text = text.lower()
    text = re.sub("[^A-Za-zА-Яа-я]", " ", text)
    text = word_tokenize(text)
    text = [word for word in text if word not in set(nltk.corpus.stopwords.words("russian"))]
    text = [lemmatizer.lemmatize(word) for word in text]
    text = ' '.join(text)
    return text

def get_word_count(text):
    text = text.lower()
    text = re.sub("[^A-Za-zА-Яа-я]", " ", text)
    text = word_tokenize(text)
    return(len(text))

def map_data(input):
    data = {}
    data['text'] = list(map(lambda x: preprocess_text(x['text']), input))
    data['photo'] = list(map(lambda x: len(x['images']), input))
    data['video'] = list(map(lambda x: len(x['videos']), input))
    data['audio'] = list(map(lambda x: len(x['audios']), input))
    data['colour'] = list(map(lambda x: first_photo_average_colour(x['images']), input))
    data['words'] = list(map(lambda x: get_word_count(x['text']), input))
    return data

def vectorize_texts(texts):
    texts_count = len(texts)
    matrix = pd.DataFrame(np.zeros((texts_count, word_features_count), dtype=int), columns=word_features)
    for i in range(texts_count):
        for word in texts[i]:
            if word in vocabulary:
                matrix['word_' + word] += 1
    return matrix

@app.route('/predict', methods=['GET', 'POST'])
def predict_virality():
    input = request.get_json()
    data = pd.DataFrame(map_data(input))
    colours = pd.DataFrame(list(data['colour']), columns=["red", "green", 'blue'])
    vectorized_texts = vectorize_texts(data['text'])
    to_predict = data[['photo', 'video', 'audio', 'words']]
    to_predict = pd.merge(to_predict, vectorized_texts, left_index=True, right_index=True)
    to_predict = pd.merge(to_predict, colours, left_index=True, right_index=True)
    result = cb.predict(to_predict)
    return jsonify(list(result))