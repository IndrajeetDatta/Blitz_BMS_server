import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargePointConfigurationComponent } from './charge-point-configuration.component';

describe('ChargePointConfigurationComponent', () => {
  let component: ChargePointConfigurationComponent;
  let fixture: ComponentFixture<ChargePointConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargePointConfigurationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargePointConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
