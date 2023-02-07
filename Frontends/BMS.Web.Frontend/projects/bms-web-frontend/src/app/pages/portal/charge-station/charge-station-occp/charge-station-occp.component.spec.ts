import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationOccpComponent } from './charge-station-occp.component';

describe('ChargeStationOccpComponent', () => {
  let component: ChargeStationOccpComponent;
  let fixture: ComponentFixture<ChargeStationOccpComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationOccpComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationOccpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
