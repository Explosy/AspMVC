<template>
	<header>
        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div class="container">
                <a class="navbar-brand">VueProject</a>
                <div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                    <ul class="navbar-nav flex-grow-1">
                        <li class="nav-item">
                            <router-link to="/" class="nav-link text-dark">Пользователи</router-link>
                        </li>
                        <li class="nav-item">
                            <router-link to="/create" class="nav-link text-dark">Добавить пользователя</router-link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
</template>


<script lang="ts">
import { defineComponent } from 'vue';
export default defineComponent({

})
</script>>
