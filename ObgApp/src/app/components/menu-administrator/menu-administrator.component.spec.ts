import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MenuAdministratorComponent } from './menu-administrator.component';

describe('MenuAdministratorComponent', () => {
  let component: MenuAdministratorComponent;
  let fixture: ComponentFixture<MenuAdministratorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MenuAdministratorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MenuAdministratorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
