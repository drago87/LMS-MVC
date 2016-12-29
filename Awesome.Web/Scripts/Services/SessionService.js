var SessionService = function($cookies){
	this.token = undefined;
	
	this.getToken = function(){
		if(!$cookies.AwesomeAngularWebAppToken){
			if(!this.token){
				return undefined;
			}
			this.setToken(this.token);			
		}
		return $cookies.AwesomeAngularWebAppToken;
	}
	
	this.setToken = function(token){
		this.token = token;
		$cookies.AwesomeAngularWebAppToken = token;
	}
	
	this.apiUrl = 'http://localhost:40000';
}

SessionService.$inject = ['$cookies'];