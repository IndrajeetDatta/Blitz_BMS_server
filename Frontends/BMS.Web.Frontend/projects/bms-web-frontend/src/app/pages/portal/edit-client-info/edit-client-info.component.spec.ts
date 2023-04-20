import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EditClientInfoComponent } from './edit-client-info.component';

describe('EditUserInfoComponent', () => {
  let component: EditClientInfoComponent;
  let fixture: ComponentFixture<EditClientInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EditClientInfoComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(EditClientInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
