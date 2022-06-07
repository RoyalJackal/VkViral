import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupsByNameComponent } from './groups-by-name.component';

describe('GroupsByNameComponent', () => {
  let component: GroupsByNameComponent;
  let fixture: ComponentFixture<GroupsByNameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GroupsByNameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupsByNameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
