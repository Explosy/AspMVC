<template>
	<h1>Детали</h1>
	<div>
		<h4>Пользователь</h4>
		<hr />
		<dl class="row">
			<dt class = "col-sm-2">
				<label class="control-label">Имя</label>
			</dt>
			<dd class = "col-sm-10">
				<label class="control-label">{{user.name}}</label>
			</dd>
			<dt class = "col-sm-2">
				<label class="control-label">Фамилия</label>
			</dt>
			<dd class = "col-sm-10">
				<label class="control-label">{{user.surname}}</label>
			</dd>
			<dt class = "col-sm-2">
				<label class="control-label">Возраст</label>
			</dt>
			<dd class = "col-sm-10">
				<label class="control-label">{{user.age}}</label>
			</dd>
			<dt class = "col-sm-2">
				<label class="control-label">Email</label>
			</dt>
			<dd class = "col-sm-10">
				<label class="control-label">{{user.email}}</label>
			</dd>
			<dt class = "col-sm-2">
				<label class="control-label">Дата регистрации</label>
			</dt>
			<dd class = "col-sm-10">
				<label class="control-label">{{user.registationDate}}</label>
			</dd>
		</dl>
		<div>
			<button class="btn btn-secondary" @click="$router.push('/')">Вернутся назад</button>
		</div>
	</div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
	import config from '@/appconfig';
	import { get } from '@/services/ApiService';
	import { ResponseModel } from '@/models/ResponseModel';
	import { User } from '@/models/User';
	
    
    interface Data {
        user : User
    }

    export default defineComponent({
        name: 'DetailsPage',
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
			}
        }
    });
</script>