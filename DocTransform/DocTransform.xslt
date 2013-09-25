<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="text" indent="no"/>

    <xsl:template match="/">
      <xsl:variable name="assemblyName" select="doc/assembly/name"/>
      <xsl:text>#</xsl:text><xsl:value-of select="$assemblyName" disable-output-escaping="yes"/>
      <xsl:text>&#xa;</xsl:text>
      <xsl:text>&#xa;</xsl:text>
      <xsl:text>Azure.Helpers is a set of utility classes that make working with Azure Storage easier.  There are currently classes for Blob and Table storage.</xsl:text>
      <xsl:text>&#xa;</xsl:text>
      <xsl:text>&#xa;</xsl:text>


      <xsl:for-each select="doc/members/member">
        <xsl:choose>
          <xsl:when test="starts-with(@name,'T')">
            <xsl:variable name="typeName" select="substring(@name,3,string-length(@name))" />
            <xsl:text>##</xsl:text><xsl:value-of select="substring($typeName,string-length($assemblyName) + 2, string-length($typeName))"/>
            <xsl:text>&#xa;</xsl:text>

            <xsl:for-each select="/doc/members/member">
              <xsl:if test="not(starts-with(@name,'T'))">
                <xsl:if test="contains(@name,$typeName)">
                  <xsl:text>###</xsl:text><xsl:value-of select="substring(@name,string-length($assemblyName) + 4,string-length(@name))"/>
                  <xsl:text>&#xa;</xsl:text>
                  <xsl:text>&#xa;</xsl:text>
                  <xsl:value-of select="normalize-space(summary)" />
                  <xsl:text>&#xa;</xsl:text>
                  <xsl:text>&#xa;</xsl:text>
                  <xsl:if test="count(param) > 0">
                    <xsl:text>####Params</xsl:text>
                    <xsl:text>&#xa;</xsl:text>
                    <xsl:text>&#xa;</xsl:text>
                    <xsl:for-each select="param">
                      <xsl:text disable-output-escaping="yes">> **</xsl:text>
                      <xsl:value-of select="@name"/>
                      <xsl:text>**</xsl:text>
                      <xsl:text>: </xsl:text>
                      <xsl:value-of select="."/>
                      <xsl:text>&#xa;</xsl:text>
                      <xsl:text>&#xa;</xsl:text>
                    </xsl:for-each>
                  </xsl:if>
                </xsl:if>
              </xsl:if>
            </xsl:for-each>
            <xsl:text>&#xa;</xsl:text>
          </xsl:when>
        </xsl:choose>
       
      </xsl:for-each>
    </xsl:template>
</xsl:stylesheet>
