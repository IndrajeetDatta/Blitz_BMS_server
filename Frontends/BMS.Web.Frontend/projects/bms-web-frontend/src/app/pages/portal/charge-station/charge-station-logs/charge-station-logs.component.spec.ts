import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationLogsComponent } from './charge-station-logs.component';

describe('ChargeStationLogsComponent', () => {
  let component: ChargeStationLogsComponent;
  let fixture: ComponentFixture<ChargeStationLogsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationLogsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationLogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
