import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OgrenciLayoutComponent } from './ogrenci-layout.component';

describe('OgrenciLayoutComponent', () => {
  let component: OgrenciLayoutComponent;
  let fixture: ComponentFixture<OgrenciLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OgrenciLayoutComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OgrenciLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
