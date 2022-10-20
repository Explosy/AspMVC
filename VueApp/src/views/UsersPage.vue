<template>
    <div>
        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started.
        </div>

        <div v-if="users" class="content">
            <h1>Список пользователей</h1>
            <div class="row">
                <div class="col-md-3">
                    <div class="input-group">
                        <input type="text" class="form-control" placeholder="Email" aria-label="Email" aria-describedby="input-group-right" v-model="searchText">
                        <button @click="searchUsers" class="btn btn-light">
                            <IconSearch/>
                        </button>
                    </div>
                </div>
            </div>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>Имя</th>
                        <th>Фамилия</th>
                        <th>Возраст</th>
                        <th>Email</th>
                        <th>Действия</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="user in users" :key="user.id">
                        <td>{{ user.name }}</td>
                        <td>{{ user.surname }}</td>
                        <td>{{ user.age }}</td>
                        <td>{{ user.email }}</td>
                        <td>
                            <div class="btn-group" role="group" aria-label="Basic outlined button group">
                                <button type="button" class="btn btn-outline-primary" @click="$router.push(`/edit/${user.id}`)"><IconEdit/></button>
                                <button type="button" class="btn btn-outline-primary" @click="$router.push(`/details/${user.id}`)"><IconInfo/></button>
                                <button type="button" class="btn btn-outline-primary" @click="deleteUser(user)"><IconDelete/></button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import config from '@/appconfig';
    import { get, del }  from '@/services/ApiService';
    import { ResponseModel } from '@/models/ResponseModel';
    import { User }  from '@/models/User';
    import  IconEdit  from '@/assets/icons/IconEdit.vue'
    import  IconInfo  from '@/assets/icons/IconInfo.vue'
    import  IconDelete  from '@/assets/icons/IconDelete.vue'
    import  IconSearch  from '@/assets/icons/IconSearch.vue';

    type Users = User[];

    interface Data {
        loading: boolean,
        users: null | Users
        searchText: string
    }

    export default defineComponent({
        name: 'UsersPage',
        components: {
            IconEdit,
            IconInfo,
            IconDelete,
            IconSearch
        },
        data(): Data {
            return {
                loading: false,
                users: null,
                searchText: ""
            };
        },
        created() {
            this.searchUsers();
        },
        methods: {
            searchUsers(): void {
                this.users = null;
                this.loading = true;
                get<ResponseModel>(`${config.API_URL}/?Email=${this.searchText}`)
                    .then(model => {
                        this.users = model.data as Users;
                        this.loading = false;
                    });
                this.searchText = "";
            },
            deleteUser(user : User): void {
                if (!confirm(`Вы уверены что хотите удалить пользователя ${user.surname} ${user.name}?`)){
                    return;
                }
                del(`${config.API_URL}/${user.id}`)
                    .then((response) => {
                        this.searchUsers();
                        console.log(response);
                    });
            }
        },
    });
</script>