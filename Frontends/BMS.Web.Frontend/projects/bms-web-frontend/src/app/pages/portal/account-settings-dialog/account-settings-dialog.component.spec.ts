import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountSettingsDialogComponent } from './account-settings-dialog.component';

describe('AccountSettingsDialogComponent', () => {
  let component: AccountSettingsDialogComponent;
  let fixture: ComponentFixture<AccountSettingsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AccountSettingsDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AccountSettingsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
