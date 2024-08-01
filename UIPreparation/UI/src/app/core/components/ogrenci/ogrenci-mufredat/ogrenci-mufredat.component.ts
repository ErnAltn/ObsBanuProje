import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LocalStorageService } from 'app/core/services/local-storage.service';
import { AuthService } from '../../admin/login/services/auth.service';
import { ST_AkademikDonem } from '../../admin/sT_AkademikDonem/models/ST_AkademikDonem';
import { ST_AkademikYilService } from '../../admin/sT_AkademikYil/services/ST_AkademikYil.service';
import { OzlukBilgileriService } from '../ozluk-bilgileri/services/ozlukBilgileri.service';
import { OgrenciMufredatDto } from './models/ogrenciMufredat';



@Component({
  selector: 'app-ogrenci-mufredat',
  templateUrl: './ogrenci-mufredat.component.html',
  styleUrls: ['./ogrenci-mufredat.component.css']
})
export class OgrenciMufredatComponent implements OnInit {

  ogrenciMufredatList: OgrenciMufredatDto[];
  filteredMufredatList:OgrenciMufredatDto[];
  akademikYilList:ST_AkademikDonem[];

  isChange=false;
  constructor(private mufredatService: OzlukBilgileriService, private storageService:LocalStorageService,
     private authService: AuthService,private alertifyService:AlertifyService,private akademikYilService:ST_AkademikYilService) { }

  ngOnInit(): void {
    this.getOgrenciMufredatList();
    this.getAkademikYilList();
    
  }




  getAkademikYilList(){
    this.akademikYilService.getST_AkademikYilList().subscribe(data=>{
      this.akademikYilList=data.reverse();
    })
  }


  getOgrenciMufredatList() {
    this.mufredatService.getOgrenciMufredat(this.storageService.getUserId()).subscribe(data => {
     this.ogrenciMufredatList=data
   });
  }

  filterByAkademikYil(akademikYil){
    if(this.isChange==false){
      this.filteredMufredatList= this.ogrenciMufredatList.filter(a=>a.akedemikYil===akademikYil)
      this.isChange=true
      if(this.filteredMufredatList.length==0){
        this.alertifyService.error("Seçtiğiniz döneme ait ders bulunamadı!")
      }
    }
    else
    {
      this.getOgrenciMufredatList();
      this.filteredMufredatList= this.ogrenciMufredatList.filter(a=>a.akedemikYil===akademikYil)
      if(this.filteredMufredatList.length==0){
        this.alertifyService.error("Seçtiğiniz döneme ait ders bulunamadı!")
      }
      console.log(this.filteredMufredatList);
    }
  }
}
