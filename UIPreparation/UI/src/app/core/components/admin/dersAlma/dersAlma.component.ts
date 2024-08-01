import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { DersAlma } from './models/DersAlma';
import { DersAlmaService } from './services/DersAlma.service';

declare var jQuery: any;

@Component({
	selector: 'app-dersAlma',
	templateUrl: './dersAlma.component.html',
	styleUrls: ['./dersAlma.component.scss']
})
export class DersAlmaComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','dersAcmaId','ogrenciId','dersDurumId','createdDate','updatedDate','deletedDate', 'update','delete'];

	dersAlmaList:DersAlma[];
	dersAlma:DersAlma=new DersAlma();

	dersAlmaAddForm: FormGroup;


	dersAlmaId:number;

	constructor(private dersAlmaService:DersAlmaService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getDersAlmaList();
    }

	ngOnInit() {

		this.createDersAlmaAddForm();
	}


	getDersAlmaList() {
		this.dersAlmaService.getDersAlmaList().subscribe(data => {
			this.dersAlmaList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.dersAlmaAddForm.valid) {
			this.dersAlma = Object.assign({}, this.dersAlmaAddForm.value)

			if (this.dersAlma.id == 0)
				this.addDersAlma();
			else
				this.updateDersAlma();
		}

	}

	addDersAlma(){

		this.dersAlmaService.addDersAlma(this.dersAlma).subscribe(data => {
			this.getDersAlmaList();
			this.dersAlma = new DersAlma();
			jQuery('#dersalma').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.dersAlmaAddForm);

		})

	}

	updateDersAlma(){

		this.dersAlmaService.updateDersAlma(this.dersAlma).subscribe(data => {

			var index=this.dersAlmaList.findIndex(x=>x.id==this.dersAlma.id);
			this.dersAlmaList[index]=this.dersAlma;
			this.dataSource = new MatTableDataSource(this.dersAlmaList);
            this.configDataTable();
			this.dersAlma = new DersAlma();
			jQuery('#dersalma').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.dersAlmaAddForm);

		})

	}

	createDersAlmaAddForm() {
		this.dersAlmaAddForm = this.formBuilder.group({		
			id : [0],

dersAcmaId : [0, Validators.required],
ogrenciId : [0, Validators.required],
dersDurumId : [0, Validators.required]
		})
	}

	deleteDersAlma(dersAlmaId:number){
		this.dersAlmaService.deleteDersAlma(dersAlmaId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.dersAlmaList=this.dersAlmaList.filter(x=> x.id!=dersAlmaId);
			this.dataSource = new MatTableDataSource(this.dersAlmaList);
			this.configDataTable();
		})
	}

	getDersAlmaById(dersAlmaId:number){
		this.clearFormGroup(this.dersAlmaAddForm);
		this.dersAlmaService.getDersAlmaById(dersAlmaId).subscribe(data=>{
			this.dersAlma=data;
			this.dersAlmaAddForm.patchValue(data);
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

	checkClaim(claim:string):boolean{
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
