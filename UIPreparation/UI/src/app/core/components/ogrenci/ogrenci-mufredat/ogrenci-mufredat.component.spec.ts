import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OgrenciMufredatComponent } from './ogrenci-mufredat.component';

describe('OgrenciMufredatComponent', () => {
  let component: OgrenciMufredatComponent;
  let fixture: ComponentFixture<OgrenciMufredatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OgrenciMufredatComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OgrenciMufredatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
