import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { GroupsFormComponent } from './groups/groups-form/groups-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainPageComponent } from './main-page/main-page.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatSelectModule} from "@angular/material/select";
import {FormsModule} from "@angular/forms";
import { GroupsByNameComponent } from './groups/groups-by-name/groups-by-name.component';
import {MatChipsModule} from "@angular/material/chips";
import {MatIconModule} from "@angular/material/icon";
import {MatButtonModule} from "@angular/material/button";
import { HttpClientModule } from '@angular/common/http';
import { GroupListComponent } from './groups/group-list/group-list.component';
import {ScrollingModule} from "@angular/cdk/scrolling";
import {MatCardModule} from "@angular/material/card";
import { SearchGroupPipePipe } from './search-group-pipe.pipe';
import {MatInputModule} from "@angular/material/input";
import { GroupsByUserComponent } from './groups/groups-by-user/groups-by-user.component';
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatButtonToggleModule} from "@angular/material/button-toggle";
import { PostsFormComponent } from './posts/posts-form/posts-form.component';
import { PostListComponent } from './posts/post-list/post-list.component';
import { UserBoxComponent } from './user-box/user-box.component';
import {CookieService} from "ngx-cookie-service";
import { LoginPageComponent } from './login-page/login-page.component';
import { MediaBoxComponent } from './posts/media-box/media-box.component';
import { GroupsByQueryComponent } from './groups/groups-by-query/groups-by-query.component';
import { GroupsByActivityComponent } from './groups/groups-by-activity/groups-by-activity.component';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent,
    GroupsFormComponent,
    GroupsByNameComponent,
    GroupListComponent,
    SearchGroupPipePipe,
    GroupsByUserComponent,
    PostsFormComponent,
    PostListComponent,
    UserBoxComponent,
    LoginPageComponent,
    MediaBoxComponent,
    GroupsByQueryComponent,
    GroupsByActivityComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(
      [
        {path: '', component: MainPageComponent},
      ]),
    MatSidenavModule,
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    MatChipsModule,
    MatIconModule,
    MatButtonModule,
    ScrollingModule,
    MatCardModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonToggleModule
  ],
  providers: [CookieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
