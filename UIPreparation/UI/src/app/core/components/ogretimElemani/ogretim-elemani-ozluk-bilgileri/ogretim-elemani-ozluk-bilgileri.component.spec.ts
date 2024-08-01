import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OgretimElemaniOzlukBilgileriComponent } from './ogretim-elemani-ozluk-bilgileri.component';

describe('OgretimElemaniOzlukBilgileriComponent', () => {
  let component: OgretimElemaniOzlukBilgileriComponent;
  let fixture: ComponentFixture<OgretimElemaniOzlukBilgileriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OgretimElemaniOzlukBilgileriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OgretimElemaniOzlukBilgileriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
