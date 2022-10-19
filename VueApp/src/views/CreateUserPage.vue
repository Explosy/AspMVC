<template>
	<div>
		<h1>Создание нового пользователя</h1>
		<hr />
		<div class="row">
			<div class="col-md-3">
				<form>
					<div class="form-group">
						<label class="control-label">Имя</label>
						<input class="form-control" />
						<span asp-validation-for="Name" class="text-danger"></span>
					</div>
					<div class="form-group">
						<label class="control-label">Фамилия</label>
						<input class="form-control" />
					</div>
					<div class="form-group">
						<label class="control-label">Возраст</label>
						<input class="form-control" />
					</div>
					<div class="form-group">
						<label class="control-label">Email</label>
						<input class="form-control" />
					</div>
					<br/>
					<div class="form-group">
						<input type="submit" value="Создать" class="btn btn-primary" />
					</div>
				</form>
			</div>
		</div>
		<div>
			<router-link to="/" class="nav-link text-dark">Вернутся назад</router-link>
		</div>	
	</div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { IResponseModel } from '../types/IResponseModel';

    

    interface Data {
        response: null | IResponseModel
    }

    export default defineComponent({
        name: 'CreateUserPage',

        data(): Data {
            return {
                response: null
            };
        },
		created() {
			
		},
        methods: {
            createUser() : void {
				const requestOptions = {
					method: 'POST',
					headers: { 'Content-Type': 'application/json' },
					body: JSON.stringify(this.response)
				};
				fetch('/users', requestOptions);
			}
			
        }
    });
</script>