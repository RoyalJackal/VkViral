import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsByUserComponent } from './groups-by-user.component';

describe('GroupsByUserComponent', () => {
  let component: GroupsByUserComponent;
  let fixture: ComponentFixture<GroupsByUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupsByUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupsByUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
