import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { ST_SinavTuru } from './models/ST_SinavTuru';
import { ST_SinavTuruService } from './services/ST_SinavTuru.service';

declare var jQuery: any;

@Component({
	selector: 'app-sT_SinavTuru',
	templateUrl: './sT_SinavTuru.component.html',
	styleUrls: ['./sT_SinavTuru.component.scss']
})
export class ST_SinavTuruComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','ad','ekstra', 'update','delete'];

	sT_SinavTuruList:ST_SinavTuru[];
	sT_SinavTuru:ST_SinavTuru=new ST_SinavTuru();

	sT_SinavTuruAddForm: FormGroup;


	sT_SinavTuruId:number;

	constructor(private sT_SinavTuruService:ST_SinavTuruService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getST_SinavTuruList();
    }

	ngOnInit() {

		this.createST_SinavTuruAddForm();
	}


	getST_SinavTuruList() {
		this.sT_SinavTuruService.getST_SinavTuruList().subscribe(data => {
			this.sT_SinavTuruList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.sT_SinavTuruAddForm.valid) {
			this.sT_SinavTuru = Object.assign({}, this.sT_SinavTuruAddForm.value)

			if (this.sT_SinavTuru.id == 0)
				this.addST_SinavTuru();
			else
				this.updateST_SinavTuru();
		}

	}

	addST_SinavTuru(){

		this.sT_SinavTuruService.addST_SinavTuru(this.sT_SinavTuru).subscribe(data => {
			this.getST_SinavTuruList();
			this.sT_SinavTuru = new ST_SinavTuru();
			jQuery('#st_sinavturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_SinavTuruAddForm);

		})

	}

	updateST_SinavTuru(){

		this.sT_SinavTuruService.updateST_SinavTuru(this.sT_SinavTuru).subscribe(data => {

			var index=this.sT_SinavTuruList.findIndex(x=>x.id==this.sT_SinavTuru.id);
			this.sT_SinavTuruList[index]=this.sT_SinavTuru;
			this.dataSource = new MatTableDataSource(this.sT_SinavTuruList);
            this.configDataTable();
			this.sT_SinavTuru = new ST_SinavTuru();
			jQuery('#st_sinavturu').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.sT_SinavTuruAddForm);

		})

	}

	createST_SinavTuruAddForm() {
		this.sT_SinavTuruAddForm = this.formBuilder.group({		
			id : [0],
ad : ["", Validators.required],
ekstra : ["", Validators.required]
		})
	}

	deleteST_SinavTuru(sT_SinavTuruId:number){
		this.sT_SinavTuruService.deleteST_SinavTuru(sT_SinavTuruId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.sT_SinavTuruList=this.sT_SinavTuruList.filter(x=> x.id!=sT_SinavTuruId);
			this.dataSource = new MatTableDataSource(this.sT_SinavTuruList);
			this.configDataTable();
		})
	}

	getST_SinavTuruById(sT_SinavTuruId:number){
		this.clearFormGroup(this.sT_SinavTuruAddForm);
		this.sT_SinavTuruService.getST_SinavTuruById(sT_SinavTuruId).subscribe(data=>{
			this.sT_SinavTuru=data;
			this.sT_SinavTuruAddForm.patchValue(data);
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
