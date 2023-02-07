import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChargeStationUserDataComponent } from './charge-station-user-data.component';

describe('ChargeStationUserDataComponent', () => {
  let component: ChargeStationUserDataComponent;
  let fixture: ComponentFixture<ChargeStationUserDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChargeStationUserDataComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChargeStationUserDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
