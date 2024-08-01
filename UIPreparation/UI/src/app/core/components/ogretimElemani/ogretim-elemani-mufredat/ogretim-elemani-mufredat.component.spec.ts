import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OgretimElemaniMufredatComponent } from './ogretim-elemani-mufredat.component';

describe('OgretimElemaniMufredatComponent', () => {
  let component: OgretimElemaniMufredatComponent;
  let fixture: ComponentFixture<OgretimElemaniMufredatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OgretimElemaniMufredatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OgretimElemaniMufredatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
