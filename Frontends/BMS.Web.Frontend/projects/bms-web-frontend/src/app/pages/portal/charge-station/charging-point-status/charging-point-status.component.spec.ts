import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargingPointStatusComponent } from './charging-point-status.component';

describe('ChargingPointStatusComponent', () => {
  let component: ChargingPointStatusComponent;
  let fixture: ComponentFixture<ChargingPointStatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ChargingPointStatusComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ChargingPointStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
