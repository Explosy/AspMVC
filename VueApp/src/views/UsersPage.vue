<template>
    <div>
        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started.
        </div>

        <div v-if="users" class="content">
            <h1>Список пользователей</h1>
            <div> 
                <p>
                    Поиск по E-mail: <input type="text" v-model="searchText" />
                    <button @click="searchUsers" class="btn btn-light">Поиск</button>
                </p>
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
                                <button type="button" class="btn btn-outline-primary"><IconEdit/></button>
                                <button type="button" class="btn btn-outline-primary"><IconInfo/></button>
                                <button type="button" class="btn btn-outline-primary"><IconDelete/></button>
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
    import { IUserTypes } from '../types/IUserTypes';
    import  IconEdit  from '../assets/icons/IconEdit.vue'
    import  IconInfo  from '../assets/icons/IconInfo.vue'
    import  IconDelete  from '../assets/icons/IconDelete.vue'
    
    type Users = {
        user: IUserTypes
    }[];

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
            IconDelete
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

                fetch('users?Email='+ this.searchText)
                    .then(r => r.json())
                    .then(json => {
                        this.users = json.data as Users;
                        this.loading = false;
                    });
                this.searchText = "";
            }
        },
    });
</script>