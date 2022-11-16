import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExporterFormComponent } from './exporter-form.component';

describe('ExporterFormComponent', () => {
  let component: ExporterFormComponent;
  let fixture: ComponentFixture<ExporterFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExporterFormComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExporterFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
