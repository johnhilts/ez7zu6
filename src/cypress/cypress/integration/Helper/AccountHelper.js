export const loginPath = '/account/login';
export const authenticationRequiredPath = '/profile';
export const login = (username = 'john@test.com', password = 'test123') => {
    cy.get('#txtUserName').type(username).should('have.value', username);
    cy.get('#txtPassword').type(password).should('have.value', password);
    cy.get('button').click();
}

export const logout = () => {
    cy.get('a.glyphicon-log-out').click()
}

export const registerPath = '/account/register';