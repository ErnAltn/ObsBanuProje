import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OgrenciAlinanDerslerComponent } from './ogrenci-alinan-dersler.component';

describe('OgrenciAlinanDerslerComponent', () => {
  let component: OgrenciAlinanDerslerComponent;
  let fixture: ComponentFixture<OgrenciAlinanDerslerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OgrenciAlinanDerslerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OgrenciAlinanDerslerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
