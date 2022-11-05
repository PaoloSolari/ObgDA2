import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuOwnerComponent } from './menu-owner.component';

describe('MenuOwnerComponent', () => {
  let component: MenuOwnerComponent;
  let fixture: ComponentFixture<MenuOwnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MenuOwnerComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
