import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OcppMessageDialogComponent } from './ocpp-message-dialog.component';

describe('OcppMessageDialogComponent', () => {
  let component: OcppMessageDialogComponent;
  let fixture: ComponentFixture<OcppMessageDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OcppMessageDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OcppMessageDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
