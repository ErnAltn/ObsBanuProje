import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { Bolum } from '../bolum/models/Bolum';
import { BolumService } from '../bolum/services/Bolum.service';
import { ST_OgrenciDurum } from '../sT_OgrenciDurum/models/ST_OgrenciDurum';
import { ST_OgrenciDurumService } from '../sT_OgrenciDurum/services/ST_OgrenciDurum.service';
import { User } from '../user/models/user';
import { UserService } from '../user/services/user.service';
import { Ogrenci } from './models/Ogrenci';
import { OgrenciOzlukBilgileriDto } from './models/ogrenciOzlukBilgileriDto';
import { OgrenciService } from './services/Ogrenci.service';

declare var jQuery: any;

@Component({
	selector: 'app-ogrenci',
	templateUrl: './ogrenci.component.html',
	styleUrls: ['./ogrenci.component.scss']
})
export class OgrenciComponent implements AfterViewInit, OnInit {

	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'bolumId', 'ogrenciNo', 'durumId', 'ayrilmaTarihi', 'adi', 'soyadi', 'tcKimlikNo', 'cinsiyet', 'dogumTarihi', 'userId', 'createdDate', 'updatedDate', 'deletedDate', 'update', 'delete'];

	ogrenciList: Ogrenci[];
	ogrenci: Ogrenci = new Ogrenci();
	ogrenciDurumList: ST_OgrenciDurum[];
	bolumList: Bolum[];
	userList: User[];
	ogrenciAddForm: FormGroup;
	ozlukBilgisi: OgrenciOzlukBilgileriDto;


	ogrenciId: number;

	constructor(private ogrenciService: OgrenciService, private lookupService: LookUpService, private ogrenciDurumService: ST_OgrenciDurumService, private userService: UserService
		, private bolumService: BolumService, private alertifyService: AlertifyService, private formBuilder: FormBuilder, private authService: AuthService) { }

	ngAfterViewInit(): void {
		this.getOgrenciList();
	}

	ngOnInit() {

		this.createOgrenciAddForm();
		this.getOgrenciDurumList();
		this.getBolumList();
		this.getUserList();
	}


	getOgrenciList() {
		this.ogrenciService.getOgrenciList().subscribe(data => {
			this.ogrenciList = data;
			this.dataSource = new MatTableDataSource(data);
			this.configDataTable();
		});
	}

	getOgrenciDurumList() {
		this.ogrenciDurumService.getST_OgrenciDurumList().subscribe(data => {
			this.ogrenciDurumList = data
		})
	}

	getUserList() {
		this.userService.getUserList().subscribe(data => {
			this.userList = data.filter(u => u.notes == "Öğrenci");
		})
	}

	getBolumList() {
		this.bolumService.getBolumList().subscribe(data => {
			this.bolumList = data
		})
	}

	

	save() {

		if (this.ogrenciAddForm.valid) {
			this.ogrenci = Object.assign({}, this.ogrenciAddForm.value)

			if (this.ogrenci.id == 0)
				this.addOgrenci();
			else
				this.updateOgrenci();
		}

	}

	manOrWomen(gender: number) {
		if (gender == 0)
			return 'Erkek'
		else
			return 'Kız'
	}


	addOgrenci() {

		this.ogrenciService.addOgrenci(this.ogrenci).subscribe(data => {
			this.getOgrenciList();
			this.ogrenci = new Ogrenci();
			jQuery('#ogrenci').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ogrenciAddForm);

		})

	}

	updateOgrenci() {

		this.ogrenciService.updateOgrenci(this.ogrenci).subscribe(data => {

			var index = this.ogrenciList.findIndex(x => x.id == this.ogrenci.id);
			this.ogrenciList[index] = this.ogrenci;
			this.dataSource = new MatTableDataSource(this.ogrenciList);
			this.configDataTable();
			this.ogrenci = new Ogrenci();
			jQuery('#ogrenci').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.ogrenciAddForm);
		})

	}

	createOgrenciAddForm() {
		this.ogrenciAddForm = this.formBuilder.group({
			id: [0],
			bolumId: [0, Validators.required],
			ogrenciNo: ["", Validators.required],
			durumId: [0, Validators.required],
			adi: ["", Validators.required],
			soyadi: ["", Validators.required],
			tcKimlikNo: ["", Validators.required],
			dogumTarihi: [null, Validators.required],
			userId: [0, Validators.required]
		})
	}

	deleteOgrenci(ogrenciId: number) {
		this.ogrenciService.deleteOgrenci(ogrenciId).subscribe(data => {
			this.alertifyService.success(data.toString());
			this.ogrenciList = this.ogrenciList.filter(x => x.id != ogrenciId);
			this.dataSource = new MatTableDataSource(this.ogrenciList);
			this.configDataTable();
		})
	}

	getOgrenciById(ogrenciId: number) {
		this.clearFormGroup(this.ogrenciAddForm);
		this.ogrenciService.getOgrenciById(ogrenciId).subscribe(data => {
			this.ogrenci = data;
			this.ogrenciAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim: string): boolean {
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

}
