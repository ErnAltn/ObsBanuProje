import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AuthService } from '../../admin/login/services/auth.service';
import { OgretimElemaniServiceIn } from '../services/ogretimElemani.service';
import { OgretimElemaniOzlukBilgileriDto } from './models/ogretimElemaniOzlukBilgileriDto';

@Component({
  selector: 'app-ogretim-elemani-ozluk-bilgileri',
  templateUrl: './ogretim-elemani-ozluk-bilgileri.component.html',
  styleUrls: ['./ogretim-elemani-ozluk-bilgileri.component.css']
})
export class OgretimElemaniOzlukBilgileriComponent implements OnInit {

  constructor(private ozlukBilgileriService: OgretimElemaniServiceIn, private fb: FormBuilder,private authService:AuthService) { }

  ozlukBilgileriform: FormGroup;
  ozlukBilgisi: OgretimElemaniOzlukBilgileriDto;

  ngOnInit(): void {
    this.createOzlukBilgilerForm();
    this.getOzlukBilgileri(11);
  }



  getOzlukBilgileri(id:number) {
    this.ozlukBilgileriService.getOgretimElemaniOzlukBilgileri(id).subscribe(data => {
      this.ozlukBilgisi = data
      this.ozlukBilgileriform.patchValue(this.ozlukBilgisi[0]);
    })
  }

  createOzlukBilgilerForm() {
		this.ozlukBilgileriform = this.fb.group({
      adi: [{ value: '', disabled: true }],
      soyadi: [{ value: '', disabled: true }],
      tcKimlikNo: [{ value: '', disabled: true }],
      dogumTarihi: [{ value: '', disabled: true }],
      bolumAdi: [{ value: '', disabled: true }],
      adres: [{ value: '', disabled: true }],
      telefonNo: [{ value: '', disabled: true }],
      unvan: [{value: '', disabled:true}],
      kurumSicilNo: [{value: '', disabled:true}],
      kayitTarihi: [{value: '', disabled:true}]
    });
	}

}
