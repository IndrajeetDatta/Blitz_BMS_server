import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationEmailsComponent } from './charge-station-emails.component';

describe('ChargeStationEmailsComponent', () => {
  let component: ChargeStationEmailsComponent;
  let fixture: ComponentFixture<ChargeStationEmailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationEmailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationEmailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
