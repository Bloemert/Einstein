<template>
    <div>
    <div class="panel-heading">
        <nav class="tile is-parent">
            <!-- Left side -->
            <div class="tile is-child is-4">
                <div class="columns">
                    <div class="column is-narrow">
                        <b-icon class="is-large fa-3x" pack="fa" icon="universal-access" />
                    </div>
                    <div class="column is-6">
                        <p class="title">
                            Einstein
                        </p>
                        <p class="subtitle is-tiny">
                            Tech Radar
                        </p>
                    </div>
                </div>
            </div>

            <!-- Center -->
            <div class="tile is-child is-6">
                <b-field style="margin-top:15px;">
                    <b-input placeholder="Search..."
                             type="search"
                             icon-pack="fa"
                             icon="search">
                    </b-input>
                </b-field>
            </div>


            <!-- Right side -->
            <div class="tile is-child is-2">
                <b-dropdown class="is-pulled-right" style="margin-top:15px;">
                    <a class="navbar-item" slot="trigger">
                        <div class="columns">
                            <div class="column">
                                <b-icon pack="fa fa-2x" icon="user-circle"></b-icon>
                            </div>
                            <div class="column">
                                <b-icon pack="fa" icon="caret-down"></b-icon>
                            </div>
                        </div>
                    </a>

                    <b-dropdown-item custom>
                        Henry Roeland
                    </b-dropdown-item>
                    <hr class="dropdown-divider">
                    <b-dropdown-item value="home">
                        <b-icon pack="fa" icon="home" /> Home
                    </b-dropdown-item>
                    <hr class="dropdown-divider">
                    <b-dropdown-item value="settings">
                        <div class="field">
                            <b-icon pack="fa" icon="cogs" /><b-switch v-model="isOpen"> Settings</b-switch>
                        </div>
                    </b-dropdown-item>
                    <b-dropdown-item value="Logout">
                        <b-icon pack="fa" icon="power-off" /> Logout
                    </b-dropdown-item>
                </b-dropdown>
            </div>
        </nav>
        </div>

        <b-collapse class="panel" :open.sync="isOpen">
            <b-tabs>
                <b-tab-item label="User management">
                    <users-table/>
                </b-tab-item>
                <b-tab-item label="Knowledge basics">
                    Set some basic knowledge settings...
                </b-tab-item>
             </b-tabs>
        </b-collapse>
    </div>

</template>

<script>
    import Vue from 'vue';
    import UsersTable from './auth/UsersTable.vue';

    export default Vue.extend({
        name: 'NavMenu',
        data() {
            return {
                isOpen: true
            }
        },
        components: {
            'users-table': UsersTable
        }
    });
</script>

