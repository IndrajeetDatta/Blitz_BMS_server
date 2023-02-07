import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationWhitelistComponent } from './charge-station-whitelist.component';

describe('ChargeStationWhitelistComponent', () => {
  let component: ChargeStationWhitelistComponent;
  let fixture: ComponentFixture<ChargeStationWhitelistComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationWhitelistComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationWhitelistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
