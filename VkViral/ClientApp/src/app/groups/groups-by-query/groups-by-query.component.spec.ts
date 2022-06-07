import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsByQueryComponent } from './groups-by-query.component';

describe('GroupsByQueryComponent', () => {
  let component: GroupsByQueryComponent;
  let fixture: ComponentFixture<GroupsByQueryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupsByQueryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupsByQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
