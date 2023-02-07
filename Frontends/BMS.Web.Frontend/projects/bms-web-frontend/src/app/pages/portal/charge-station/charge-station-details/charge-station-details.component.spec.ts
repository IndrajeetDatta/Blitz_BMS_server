import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationDetailsComponent } from './charge-station-details.component';

describe('ChargeStationDetailsComponent', () => {
  let component: ChargeStationDetailsComponent;
  let fixture: ComponentFixture<ChargeStationDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
