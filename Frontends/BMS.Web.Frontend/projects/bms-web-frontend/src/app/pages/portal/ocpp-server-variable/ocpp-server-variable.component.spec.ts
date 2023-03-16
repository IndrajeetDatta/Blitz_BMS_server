import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OcppServerVariableComponent } from './ocpp-server-variable.component';

describe('OcppServerVariableComponent', () => {
  let component: OcppServerVariableComponent;
  let fixture: ComponentFixture<OcppServerVariableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OcppServerVariableComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(OcppServerVariableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
