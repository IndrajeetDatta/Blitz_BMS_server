import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargingPointEditComponent } from './charging-point-edit.component';

describe('ChargingPointEditComponent', () => {
  let component: ChargingPointEditComponent;
  let fixture: ComponentFixture<ChargingPointEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargingPointEditComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargingPointEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
