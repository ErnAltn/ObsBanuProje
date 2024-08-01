import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OgrenciDersProgramiComponent } from './ogrenci-ders-programi.component';

describe('OgrenciDersProgramiComponent', () => {
  let component: OgrenciDersProgramiComponent;
  let fixture: ComponentFixture<OgrenciDersProgramiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OgrenciDersProgramiComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OgrenciDersProgramiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
