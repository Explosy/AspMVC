<template>
	<div class="container">
		<h1>Создание нового пользователя</h1>
		<hr />
		<div class="row">
			<div class="col-md-4">
				<form>
					<div class="form-group">
						<label class="control-label"></label>
						<input class="form-control" />
						<span asp-validation-for="Name" class="text-danger"></span>
					</div>
					<div class="form-group">
						<label class="control-label"></label>
						<input class="form-control" />
					</div>
					<div class="form-group">
						<label class="control-label"></label>
						<input class="form-control" />
					</div>
					<div class="form-group">
						<label class="control-label"></label>
						<input class="form-control" />
					</div>
					<div class="form-group">
						<input type="submit" value="Создать" class="btn btn-primary" />
					</div>
				</form>
			</div>
		</div>
		<div>
			<a asp-action="Index">Вернутся назад</a>
		</div>	
	</div>
</template>
