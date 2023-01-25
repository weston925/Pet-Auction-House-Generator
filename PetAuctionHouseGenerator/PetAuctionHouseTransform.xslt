<xsl:stylesheet id="CPF" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="/">
<style>
    body {
        background-image: url("http://i.imgur.com/IfYf93p.jpg");
        background-repeat: no-repeat;
        background-attachment: fixed;
        background-position: center;
        font: 10pt verdana;
        background-size: cover;
    }
    h1 {
        font: 30pt verdana;
        font-weight: bold;
        margin: 0;
        padding: 20px 0;
    }
    #pah {
        text-align: center;
        background-color: white;
        width: 700px;
        margin: 0 auto;
        padding: 0px 5px;
    }
    .pah_user {
        padding: 5px 0;
        border-bottom: 1px solid blue;
    }
    .pah_pet {
        display: inline-block;
		width: 300px;
        font-size: 12pt;
        margin-bottom: 5px;
		padding: 10px;
		background-color: #89CFF0;
    }
    .pah_pet a {
        display: block;
    }
	.pah_pet img {
	    width: 300px;
	}
    .pah_user_lookup {
        display: block;
        font-size: 10pt;
    }
	.pah_pet_text {
	    margin-top: 5px;
	}
	.pah_credits {
	    padding: 10 0;
		font-size: 8pt;
	}
</style>
<div id="pah">
    <h1>The Pot</h1>
    <xsl:for-each select="pah/User">
    <div class="pah_user">
        <xsl:for-each select="Pet">
			<div class="pah_pet">
				<xsl:if test="@Type='Standard'">
					<a href="/petlookup.phtml?pet={@PetNameUriEscaped}" title="{@PetName}">
						<img src="http://pets.neopets.com/cpn/{@PetNameUriEscaped}/1/4.png"/>
					</a>
					<span class="pah_pet_text">
						<xsl:value-of select="@PetName"/> - <xsl:value-of select="@PetType"/>
					</span>
				</xsl:if>
				<xsl:if test="@Type='Custom'">
					<xsl:choose>
						<xsl:when test="@ImageLink">
							<a href="{@ImageLink}">
								<img src="{@ImageUrl}"/>
							</a>
						</xsl:when>
						<xsl:otherwise>
							<img src="{@ImageUrl}"/>
						</xsl:otherwise>
					</xsl:choose>
					<xsl:if test="@PetText">
						<span class="pah_pet_text">
							<xsl:value-of select="@PetText"/>
						</span>
					</xsl:if>
				</xsl:if>
			</div>
        </xsl:for-each>
        <a class="pah_user_lookup" href="/userlookup.phtml?user={@UserNameUriEscaped}">@<xsl:value-of select="@UserName"/></a>
    </div>
    </xsl:for-each>
	<div class="pah_credits">
		Layout by <a href="/userlookup.phtml?user=robin1234504">@robin1234504</a> - Coding by <a href="/userlookup.phtml?user=wesside925">@wesside925</a>
    </div>
</div>
</xsl:template>
</xsl:stylesheet>
