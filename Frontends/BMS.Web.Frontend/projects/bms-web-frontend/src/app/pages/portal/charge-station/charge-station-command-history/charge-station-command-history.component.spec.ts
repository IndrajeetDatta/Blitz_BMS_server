import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationCommandHistoryComponent } from './charge-station-command-history.component';

describe('ChargeStationCommandHistoryComponent', () => {
  let component: ChargeStationCommandHistoryComponent;
  let fixture: ComponentFixture<ChargeStationCommandHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationCommandHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationCommandHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
