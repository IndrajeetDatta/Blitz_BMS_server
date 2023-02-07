import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CellTableStationConfigurationComponent } from './cell-table-station-configuration.component';

describe('CellTableStationConfigurationComponent', () => {
  let component: CellTableStationConfigurationComponent;
  let fixture: ComponentFixture<CellTableStationConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CellTableStationConfigurationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CellTableStationConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
