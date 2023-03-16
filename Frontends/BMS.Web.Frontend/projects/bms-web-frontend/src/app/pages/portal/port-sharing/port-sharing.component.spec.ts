import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PortSharingComponent } from './port-sharing.component';

describe('PortSharingComponent', () => {
  let component: PortSharingComponent;
  let fixture: ComponentFixture<PortSharingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PortSharingComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PortSharingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
