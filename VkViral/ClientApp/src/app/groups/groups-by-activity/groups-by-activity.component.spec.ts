import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsByActivityComponent } from './groups-by-activity.component';

describe('GroupsByActivityComponent', () => {
  let component: GroupsByActivityComponent;
  let fixture: ComponentFixture<GroupsByActivityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupsByActivityComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupsByActivityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
