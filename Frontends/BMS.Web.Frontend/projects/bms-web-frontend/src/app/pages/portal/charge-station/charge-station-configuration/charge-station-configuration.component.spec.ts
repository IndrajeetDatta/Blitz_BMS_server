import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationConfigurationComponent } from './charge-station-configuration.component';

describe('ChargeStationConfigurationComponent', () => {
  let component: ChargeStationConfigurationComponent;
  let fixture: ComponentFixture<ChargeStationConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationConfigurationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
