import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationLoadManagementComponent } from './charge-station-load-management.component';

describe('ChargeStationLoadManagementComponent', () => {
  let component: ChargeStationLoadManagementComponent;
  let fixture: ComponentFixture<ChargeStationLoadManagementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationLoadManagementComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationLoadManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
