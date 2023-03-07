import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserAccessDialogComponent } from './user-access-dialog.component';

describe('UserAccessDialogComponent', () => {
  let component: UserAccessDialogComponent;
  let fixture: ComponentFixture<UserAccessDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserAccessDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserAccessDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
