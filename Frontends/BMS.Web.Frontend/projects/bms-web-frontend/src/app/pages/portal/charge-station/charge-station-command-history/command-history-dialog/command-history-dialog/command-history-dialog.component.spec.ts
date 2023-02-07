import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommandHistoryDialogComponent } from './command-history-dialog.component';

describe('CommandHistoryDialogComponent', () => {
  let component: CommandHistoryDialogComponent;
  let fixture: ComponentFixture<CommandHistoryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CommandHistoryDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommandHistoryDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
