import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MediaBoxComponent } from './media-box.component';

describe('MediaBoxComponent', () => {
  let component: MediaBoxComponent;
  let fixture: ComponentFixture<MediaBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MediaBoxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MediaBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
