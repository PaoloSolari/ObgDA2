import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicineItemComponent } from './medicine-item.component';

describe('MedicineItemComponent', () => {
  let component: MedicineItemComponent;
  let fixture: ComponentFixture<MedicineItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicineItemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicineItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
