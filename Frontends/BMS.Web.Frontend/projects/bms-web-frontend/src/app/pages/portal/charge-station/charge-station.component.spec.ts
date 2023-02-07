import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationComponent } from './charge-station.component';

describe('ChargeStationComponent', () => {
  let component: ChargeStationComponent;
  let fixture: ComponentFixture<ChargeStationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
