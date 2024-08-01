import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { AuthService } from '../../admin/login/services/auth.service';
import { ST_AkademikYil } from '../../admin/sT_AkademikYil/models/ST_AkademikYil';
import { ST_AkademikYilService } from '../../admin/sT_AkademikYil/services/ST_AkademikYil.service';
import { OgretimElemaniServiceIn } from '../services/ogretimElemani.service';
import { OgretimElemaniMufredatDto } from './models/mufredatDto';

@Component({
  selector: 'app-ogretim-elemani-mufredat',
  templateUrl: './ogretim-elemani-mufredat.component.html',
  styleUrls: ['./ogretim-elemani-mufredat.component.css']
})
export class OgretimElemaniMufredatComponent implements OnInit {
  mufredatList: OgretimElemaniMufredatDto[];
  filteredMufredatList: OgretimElemaniMufredatDto[];
  akademikYilList:ST_AkademikYil[];
  isChange=false;



  constructor(private ogretimElemaniService: OgretimElemaniServiceIn,private alertifyService:AlertifyService, private authService: AuthService,private akademikYilService:ST_AkademikYilService,private storageService:LocalStorageService ) { }

  ngOnInit(): void {
    this.getOgretimElemaniMufredat();
    this.getAkademikYilList();
  }


  getOgretimElemaniMufredat() {
    this.ogretimElemaniService.getOgretimElemaniMufredat(this.storageService.getUserId()).subscribe(data => {
      this.mufredatList = data
    });
  }


  getAkademikYilList(){
    this.akademikYilService.getST_AkademikYilList().subscribe(data=>{
      this.akademikYilList=data.reverse();
    })
  }

  filterByAkademikYil(akademikYil){
    if(this.isChange==false){
      this.filteredMufredatList= this.mufredatList.filter(a=>a.akedemikYil===akademikYil)
      this.isChange=true
      if(this.filteredMufredatList.length==0){
        this.alertifyService.error("Seçtiğiniz döneme ait ders bulunamadı!")
      }
    }
    else
    {
      this.getOgretimElemaniMufredat();
      this.filteredMufredatList= this.mufredatList.filter(a=>a.akedemikYil===akademikYil)
      if(this.filteredMufredatList.length==0){
        this.alertifyService.error("Seçtiğiniz döneme ait ders bulunamadı!")
      }
      console.log(this.filteredMufredatList);
    }
  }








}
