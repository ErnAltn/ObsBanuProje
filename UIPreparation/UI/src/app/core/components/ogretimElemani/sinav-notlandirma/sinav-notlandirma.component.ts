import { Component, OnInit } from '@angular/core';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { AuthService } from '../../admin/login/services/auth.service';
import { OgretimElemaniServiceIn } from '../services/ogretimElemani.service';
import { OgretimElemaniSinavDto } from './models/ogretimElemaniSinavlarDto';
import { DegerlendirmeDto } from './models/sinavOgrenciDto';

@Component({
  selector: 'app-sinav-notlandirma',
  templateUrl: './sinav-notlandirma.component.html',
  styleUrls: ['./sinav-notlandirma.component.css']
})
export class SinavNotlandirmaComponent implements OnInit {

  sinavList: OgretimElemaniSinavDto[];
  degerlendirmeOgrencileri: DegerlendirmeDto[];
  isTableShow: string;

  constructor(private ogretimElemaniService: OgretimElemaniServiceIn, private authService: AuthService,private storageService:LocalStorageService) { }

  ngOnInit(): void {
    this.getOgretimElemaniSinavlar();
  }



 async getOgretimElemaniSinavlar() {
    this.ogretimElemaniService.getOgretimElemaniSinav(this.storageService.getUserId()).subscribe(data => {
      this.sinavList = data;
    })
  };

  getSinavOgrencileri(id: number) {
    this.ogretimElemaniService.getSinavOgrencileri(id).subscribe(data => {
      this.degerlendirmeOgrencileri = data;
    });
  };



  showTable(sinavTuru:string): void {
    if(sinavTuru=="Vize")
    this.isTableShow="Vize";
    else if(sinavTuru=="Final")
    this.isTableShow="Final"

  }
}
