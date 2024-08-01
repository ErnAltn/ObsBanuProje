import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OzlukBilgileriComponent } from './ozluk-bilgileri.component';

describe('OzlukBilgileriComponent', () => {
  let component: OzlukBilgileriComponent;
  let fixture: ComponentFixture<OzlukBilgileriComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OzlukBilgileriComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OzlukBilgileriComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
