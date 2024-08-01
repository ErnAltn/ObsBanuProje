import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRippleModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { GroupComponent } from 'app/core/components/admin/group/group.component';
import { LoginComponent } from 'app/core/components/admin/login/login.component';
import { UserComponent } from 'app/core/components/admin/user/user.component';
import { TranslationService } from 'app/core/services/translation.service';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { BolumComponent } from '../components/admin/bolum/bolum.component';
import { DanismanlikComponent } from '../components/admin/danismanlik/danismanlik.component';
import { DegerlendirmeComponent } from '../components/admin/degerlendirme/degerlendirme.component';
import { DersAcmaComponent } from '../components/admin/dersAcma/dersAcma.component';
import { DersAlmaComponent } from '../components/admin/dersAlma/dersAlma.component';
import { DersHavuzuComponent } from '../components/admin/dersHavuzu/dersHavuzu.component';
import { DerslikComponent } from '../components/admin/derslik/derslik.component';
import { DersProgramiComponent } from '../components/admin/dersProgrami/dersProgrami.component';
import { LanguageComponent } from '../components/admin/language/language.component';
import { MufredatComponent } from '../components/admin/mufredat/mufredat.component';
import { OgrenciComponent } from '../components/admin/ogrenci/ogrenci.component';
import { OgretimElemaniComponent } from '../components/admin/ogretimElemani/ogretimElemani.component';
import { OperationClaimComponent } from '../components/admin/operationclaim/operationClaim.component';
import { SinavComponent } from '../components/admin/sinav/sinav.component';
import { ST_AkademikDonemComponent } from '../components/admin/sT_AkademikDonem/sT_AkademikDonem.component';
import { ST_AkademikYilComponent } from '../components/admin/sT_AkademikYil/sT_AkademikYil.component';
import { ST_DersAlmaDurumuComponent } from '../components/admin/sT_DersAlmaDurumu/sT_DersAlmaDurumu.component';
import { ST_DersDiliComponent } from '../components/admin/sT_DersDili/sT_DersDili.component';
import { ST_DersGunuComponent } from '../components/admin/sT_DersGunu/sT_DersGunu.component';
import { ST_DerslikTuruComponent } from '../components/admin/sT_DerslikTuru/sT_DerslikTuru.component';
import { ST_DersSeviyesiComponent } from '../components/admin/sT_DersSeviyesi/sT_DersSeviyesi.component';
import { ST_DersTuruComponent } from '../components/admin/sT_DersTuru/sT_DersTuru.component';
import { ST_OgrenciDurumComponent } from '../components/admin/sT_OgrenciDurum/sT_OgrenciDurum.component';
import { ST_OgretimDiliComponent } from '../components/admin/sT_OgretimDili/sT_OgretimDili.component';
import { ST_OgretimTuruComponent } from '../components/admin/sT_OgretimTuru/sT_OgretimTuru.component';
import { ST_ProgramTuruComponent } from '../components/admin/sT_ProgramTuru/sT_ProgramTuru.component';
import { ST_SinavTuruComponent } from '../components/admin/sT_SinavTuru/sT_SinavTuru.component';
import { TranslateComponent } from '../components/admin/translate/translate.component';
import { DashboardComponent } from '../components/app/dashboard/dashboard.component';
import { AdminLayoutRoutes } from '../components/app/layouts/admin-layout/admin-layout.routing';
import { OzlukBilgileriComponent } from '../components/ogrenci/ozluk-bilgileri/ozluk-bilgileri.component';


// export function layoutHttpLoaderFactory(http: HttpClient) {
// 
//   return new TranslateHttpLoader(http,'../../../../../../assets/i18n/','.json');
// }

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(AdminLayoutRoutes),
        FormsModule,
        ReactiveFormsModule,
        MatButtonModule,
        MatRippleModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatTooltipModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatCheckboxModule,
        NgbModule,
        NgMultiSelectDropDownModule,
        SweetAlert2Module,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                //useFactory:layoutHttpLoaderFactory,
                useClass: TranslationService,
                deps: [HttpClient]
            }
        })
    ],
    declarations: [
        DashboardComponent,
        UserComponent,
        LoginComponent,
        GroupComponent,
        LanguageComponent,
        TranslateComponent,
        OperationClaimComponent,
        BolumComponent,
        DanismanlikComponent,
        DegerlendirmeComponent,
        DersAcmaComponent,
        DersAlmaComponent,
        DersHavuzuComponent,
        DerslikComponent,
        DersProgramiComponent,
        MufredatComponent,
        OgrenciComponent,
        OgretimElemaniComponent,
        SinavComponent,
        ST_AkademikDonemComponent,
        ST_AkademikYilComponent,
        ST_DersAlmaDurumuComponent,
        ST_DersDiliComponent,
        ST_DersGunuComponent,
        ST_DerslikTuruComponent,
        ST_DersSeviyesiComponent,
        ST_DersTuruComponent,
        ST_OgrenciDurumComponent,
        ST_OgretimDiliComponent,
        ST_OgretimTuruComponent,
        ST_ProgramTuruComponent,
        ST_SinavTuruComponent,
        OzlukBilgileriComponent

    ]
})

export class AdminLayoutModule { }
