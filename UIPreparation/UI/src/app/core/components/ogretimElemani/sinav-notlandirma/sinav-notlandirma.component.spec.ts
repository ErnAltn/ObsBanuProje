import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SinavNotlandirmaComponent } from './sinav-notlandirma.component';

describe('SinavNotlandirmaComponent', () => {
  let component: SinavNotlandirmaComponent;
  let fixture: ComponentFixture<SinavNotlandirmaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SinavNotlandirmaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SinavNotlandirmaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
