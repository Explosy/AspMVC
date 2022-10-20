<template>
	<h1>Редактирование</h1>
	<div>
		<h4>Пользователь</h4>
		<hr />
		<div class="row">
			<div class="col-md-3">
				<div class="form-group">
					<label class="control-label">Имя</label>
					<input type="text" class="form-control" v-model="user.name"/>
				</div>
				<div class="form-group">
					<label class="control-label">Фамилия</label>
					<input type="text" class="form-control" v-model="user.surname"/>
				</div>
				<div class="form-group">
					<label class="control-label">Возраст</label>
					<input type="number" class="form-control" v-model="user.age"/>
				</div>
				<div class="form-group">
					<label class="control-label">Email</label>
					<input type="text" class="form-control" v-model="user.email"/>
				</div>
				<br/>
				<div class="form-group">
					<button class="btn btn-success" @click="EditUser">Обновить</button>
				</div>
			</div>
		</div>
		<div>
			<button class="btn btn-secondary" @click="$router.push('/')">Вернутся назад</button>
		</div>
	</div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
	import config from '@/appconfig';
	import { get, put } from '@/services/ApiService';
	import { ResponseModel } from '@/models/ResponseModel';
	import { User } from '@/models/User';
    
    interface Data {
        user : User
    }

    export default defineComponent({
        name: 'EditUserPage',

        data(): Data {
            return {
                user : new User()
            };
        },
		created() {
			this.GetUser()
		},
        methods: {
			async GetUser () : Promise<void>  {
				get<ResponseModel>(`${config.API_URL}/${this.$route.params.id}`)
					.then (response => {
						this.user = response.data as User;
				});
			},
			EditUser () : void {
				const response : ResponseModel = {
					error : null,
					stackTrace : null,
					isSuccess : true,
					data : this.user 
				}
				put<ResponseModel, ResponseModel>(`${config.API_URL}/${this.$route.params.id}`, response)
					.then(response => {
						this.user = response.data as User;
					})
			}
        }
    });
</script>